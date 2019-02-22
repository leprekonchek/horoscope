using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace _01_Lopukhina_Horoscope
{
    class HoroscopeViewModel : INotifyPropertyChanged
    {
        #region Fields
        private int _age;
        private DateTime _enteredDate;
        private string _chinaHoroscope;
        private string _westHoroscope;
        private string _zodiacSign = "you don't have your sign, you're unique";
        private string[] _chinaSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };

        private string _hbCongratulations =
            "This Birthday wish is just for you, \n And I hope it comes true: \n B e yourself, love and appreciate yourself \n I magine and achieve all you can \n R elax and take it easy \n T ake time and do whatever you want, your \n H umor, never lose it and \n D o not give up, continue going \n A nd remember, you are loved by others \n Y esterday is gone, tomorrow is not here, live today and enjoy the year.";

        #region Commands
        private RelayCommand<object> _calculateZodiacSign;
        private RelayCommand<object> _calculateChineseSign;
        #endregion
        #endregion

        #region Properties

        public string Age
        {
            get => $"Your age is {_age}";
        }

        public string China
        {
            get => _chinaHoroscope;
            set
            {
                _chinaHoroscope = value;
                OnPropertyChanged();
            }
        }

        public string West
        {
            get => _westHoroscope;
            set
            {
                _westHoroscope = value;
                OnPropertyChanged();
            }
        }

        private int CalculateAge(DateTime birthdayDate)
        {
            int age = DateTime.Today.Year - birthdayDate.Year;
            if (birthdayDate > DateTime.Today.AddYears(-age)) age--;
            if (birthdayDate >= DateTime.Today) age = 0;
            return age;
        }

        private bool CheckAgeCorrect(DateTime date)
        {
            int tday = DateTime.Today.Day;
            int tmonth = DateTime.Today.Month;
            int tyear = DateTime.Today.Year;

            if (date.Year > tyear || ((date.Day > tday && date.Month >= tmonth) ||
               (date.Day <= tday && date.Month > tmonth)))
            {
                MessageBox.Show("Hey, you haven't born yet!");
            }

            if (date.Day == tday && date.Month == tmonth)
            {
                MessageBox.Show(_hbCongratulations);
            }

            if (_age > 135)
            {
                MessageBox.Show("Hmm, you are too old to be alive! \n If you are, than congratulation and I'm pressing F");
            }

            return false;

        }

        public DateTime DateChanger
        {
            get => _enteredDate;
            set
            {
                _enteredDate = value;
                _age = CalculateAge(_enteredDate);
                West = "";
                China = "";
                CheckAgeCorrect(_enteredDate);
                OnPropertyChanged(nameof(Age));
            }

        }
        #endregion

        #region Commands

        public RelayCommand<object> WestHoroscopeCommand =>
            _calculateZodiacSign ?? (_calculateZodiacSign = new RelayCommand<object>(
                WestHoroscopeImplementation));

        public RelayCommand<object> ChinaHoroscopeCommand =>
            _calculateChineseSign ?? (_calculateChineseSign = new RelayCommand<object>(
                ChinaHoroscopeImplementation));

        #endregion

        public string ChinaHoroscope()
        {
            int index = Math.Abs(_enteredDate.Year - 1900) % 12;
            return _chinaSigns[index];
        }

        public string WestHoroscope()
        {
            int month = _enteredDate.Month;
            int day = _enteredDate.Day;

            switch (month)
            {
                case 01 when day >= 20:
                case 02 when day <= 18:
                    _zodiacSign = "Aquarius";
                    break;
                case 02 when day >= 19:
                case 03 when day <= 20:
                    _zodiacSign = "Pisces";
                    break;
                case 03 when day >= 21:
                case 04 when day <= 19:
                    _zodiacSign = "Aries";
                    break;
                case 04 when day >= 20:
                case 05 when day <= 20:
                    _zodiacSign = "Taurus";
                    break;
                case 05 when day >= 21:
                case 06 when day <= 20:
                    _zodiacSign = "Gemini";
                    break;
                case 06 when day >= 21:
                case 07 when day <= 22:
                    _zodiacSign = "Cancer";
                    break;
                case 07 when day >= 23:
                case 08 when day <= 22:
                    _zodiacSign = "Leo";
                    break;
                case 08 when day >= 23:
                case 09 when day <= 22:
                    _zodiacSign = "Virgo";
                    break;
                case 09 when day >= 23:
                case 10 when day <= 22:
                    _zodiacSign = "Libra";
                    break;
                case 10 when day >= 23:
                case 11 when day <= 21:
                    _zodiacSign = "Scorpio";
                    break;
                case 11 when day >= 22:
                case 12 when day <= 21:
                    _zodiacSign = "Sagittarius";
                    break;
                case 12 when day >= 22:
                case 01 when day <= 19:
                    _zodiacSign = "Capricorn";
                    break;
            }

            return _zodiacSign;
        }

        private async void WestHoroscopeImplementation(object obj)
        {
            await Task.Run(() => West = WestHoroscope());
        }

        private async void ChinaHoroscopeImplementation(object obj)
        {
            await Task.Run(() => China = ChinaHoroscope());
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        // [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
