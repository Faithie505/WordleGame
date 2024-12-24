using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleGame
{
    public class ViewModel : INotifyPropertyChanged
    {
        //private bool _isDarkTheme;
        private double _fontSize;
        private bool _isDarkTheme;

        public bool IsDarkTheme
        {
            get => Preferences.Get("IsDarkTheme", false);
            set
            {
                if (_isDarkTheme == value)
                {
                    return;
                }
                else if (_isDarkTheme != value)
                {
                    _isDarkTheme = value;
                    Preferences.Set("IsDarkTheme", value); // Save the state
                    OnPropertyChanged(nameof(IsDarkTheme));
                }
                
                _isDarkTheme = value;
            }
        }

        public double FontSize
        {
            get => _fontSize;
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged(nameof(FontSize));
                }
            }
        }

        public ViewModel()
        {
            // Load saved settings from Preferences
            IsDarkTheme = Preferences.Get("IsDarkTheme", true); // Default to false (light theme)
            FontSize = Preferences.Get("FontSize", 10.0); // Default font size
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SaveSettings()
        {
            // Save settings to Preferences
            Preferences.Set("IsDarkTheme", IsDarkTheme);
            Preferences.Set("FontSize", FontSize);

        }

        public void GetSettings()
        {
            // get settings from Preferences
            Preferences.Get("IsDarkTheme", IsDarkTheme);
            Preferences.Get("FontSize", FontSize);
        }
    }

}
