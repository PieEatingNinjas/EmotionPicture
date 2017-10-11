namespace TestRealmCognitiveXamarin.Images
{
    public interface IImageResizer
    {
        byte[] ResizeTheImage(byte[] imageData, float width, float height);
    }
}
