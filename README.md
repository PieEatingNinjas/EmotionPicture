# EmotionPicture
Example app that uses Realm, Realm Functions, Azure Functions and Cognitive Services to link an emotion to a picture

## Realm Function
The Realm function runs on the Realm Object server and listens for changes on a specific Realm. Whenever a picture is inserted, the Realm Funtion will call an Azure Function which will return the most present emotion. This emotion is then written to the Emotion Property of the EmotionalPicture object. The changes will be reflected on the UI immediatley.

# Azure Function
The Azure Funtion receives the picture data and sends it to Cognitive Services which will analyse the image. A collection of emotions will be returned from CS, the Azure function filters out the most present emotion and returns this as a result.