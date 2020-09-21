using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XFEFilePickerSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            // For custom file types            
            // var customFileType =
            //     new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            //     {
            //         { DevicePlatform.iOS, new[] { "com.adobe.pdf" } }, // or general UTType values
            //         { DevicePlatform.Android, new[] { "application/pdf" } },
            //         { DevicePlatform.UWP, new[] { ".pdf" } },
            //         { DevicePlatform.Tizen, new[] { "*/*" } },
            //         { DevicePlatform.macOS, new[] { "pdf"} }, // or general UTType values
            //     });

            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                //FileTypes = customFileType,
                PickerTitle = "Pick an image"
            });

            if (pickResult != null)
            {
                var stream = await pickResult.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async void Button1_Clicked(System.Object sender, System.EventArgs e)
        {
            var pickResult = await FilePicker.PickMultipleAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick image(s)"
            });

            if (pickResult != null)
            {
                var imageList = new List<ImageSource>();

                foreach(var image in pickResult)
                {
                    var stream = await image.OpenReadAsync();

                    imageList.Add(ImageSource.FromStream(() => stream));
                }

                collectionView.ItemsSource = imageList;
            }
        }
    }
}
