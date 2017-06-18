using System;
using Xamarin.Forms;

namespace FormsPhoneWord {
    public class MainPage : ContentPage {

        Entry phoneNumberText;
        Button btnTranslate;
        Button btnCall;
        String translatedNumber;
        
        public MainPage() {
            // padding around main page
            this.Padding = new Thickness(20, 20, 20, 20);
            // Stack layout with spacing of 15 inbetween children
            StackLayout panel = new StackLayout {
                Spacing = 15
            };
            // plain text (Entry)
            panel.Children.Add(phoneNumberText = new Entry {
                Text = "1-855-XAMARIN"
            });
            // button (translate)
            panel.Children.Add(btnTranslate = new Button {
                Text = "Translate"
            });
            // button (call)
            panel.Children.Add(btnCall = new Button {
                Text = "Call",
                IsEnabled = false
            });
            btnTranslate.Clicked += OnTranslate;
            btnCall.Clicked += OnCall;
            // set content equal to our panel (everything in stacklayout)
            this.Content = panel;
        }

        public void OnTranslate(object sender, EventArgs e) {
            String enteredNumbers = phoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumbers);

            if(!String.IsNullOrEmpty(translatedNumber)) {
                btnCall.Text = "Call " + translatedNumber;
                btnCall.IsEnabled = true;
            } else {
                btnCall.IsEnabled = false;
                btnCall.Text = "Call";
            }
        }

        async void OnCall(object sender, System.EventArgs e) {
            if(await this.DisplayAlert("Dial the Number", "Would you like to call " + translatedNumber + "?", "Yes", "No")) {
                var dialer = DependencyService.Get<IDialer>();
                if(dialer != null) {
                    await dialer.DialAsync(translatedNumber);
                }
            }
        }
    }
}
