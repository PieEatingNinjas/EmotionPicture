using Plugin.Media;
using Realms;
using Realms.Sync;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TestRealmCognitiveXamarin.Images;
using TestRealmCognitiveXamarin.Models;
using TestRealmCognitiveXamarin.Settings;
using Xamarin.Forms;

namespace TestRealmCognitiveXamarin.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        Realm Realm;

        EmotionalPicture _Picture;
        public EmotionalPicture Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                _Picture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Picture)));
            }
        }

        ICommand _PickImageCommand;
        public ICommand PickImageCommand => _PickImageCommand ?? (_PickImageCommand = new Command(OnPickImage));

        public MainPageViewModel()
        {
            Initialize();
        }

        private async Task Initialize()
        {
            var user = await LoginUser();
            var serverURL = new Uri(Connection.RealmServerUrl);
            var configuration = new SyncConfiguration(user, serverURL);

            Realm = Realm.GetInstance(configuration);
        }

        private async Task<User> LoginUser()
        {
            try
            {
                User user = null;

                User.ConfigurePersistence(UserPersistenceMode.NotEncrypted);

                if (User.AllLoggedIn.Count() > 1)
                    foreach (var item in User.AllLoggedIn)
                    {
                        item.LogOut();
                    }

                user = User.Current;
                if (user == null)
                {
                    var credentials = Credentials.UsernamePassword(Authentication.DefaultUser,
                        Authentication.DefaultPassword, createUser: false);

                    var authURL = new Uri(Connection.AuthUrl);
                    user = await User.LoginAsync(credentials, authURL);
                }

                return user;
            }
            catch (Exception ex)
            { }
            return null;
        }

        private async void OnPickImage(object obj)
        {
            var data = await TakePhoto();

            Picture = new EmotionalPicture()
            {
                Data = data
            };

            Realm.Write(() =>
            {
                Realm.Add(Picture);
            });
        }

        private async Task<string> TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "pic.jpg"
            });

            if (file == null)
                return null;

            byte[] ImageData = null;
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                var myfile = memoryStream.ToArray();
                ImageData = myfile;
                file.Dispose();
            }

            byte[] resizedImage = DependencyService.Get<IImageResizer>().ResizeTheImage(ImageData, 100, 100);

            ImageData = resizedImage;

            return Convert.ToBase64String(ImageData);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
