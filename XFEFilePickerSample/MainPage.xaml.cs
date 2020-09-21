using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        async void Button_Clicked(object sender, EventArgs e)
        {
            // For custom file types            
            //var customFileType =
            //    new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            //    {
            //        { DevicePlatform.iOS, new[] { "com.adobe.pdf" } }, // or general UTType values
            //        { DevicePlatform.Android, new[] { "application/pdf" } },
            //        { DevicePlatform.UWP, new[] { ".pdf" } },
            //        { DevicePlatform.Tizen, new[] { "*/*" } },
            //        { DevicePlatform.macOS, new[] { "pdf"} }, // or general UTType values
            //    });

            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a photo",
                FileTypes = FilePickerFileType.Images
                // For custom file types
                //FileTypes = customFileType
            });

            // If result == null user cancelled!
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }

        async void Button1_Clicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "Pick a photo",
                FileTypes = FilePickerFileType.Images
            });

            // If result == null user cancelled!
            if (result != null)
            {
                var images = new List<ImageSource>();

                foreach (var image in result)
                {
                    var stream = await image.OpenReadAsync();
                    images.Add(ImageSource.FromStream(() => stream));
                }

                collectionView.ItemsSource = images;
            }
        }
    }
}
