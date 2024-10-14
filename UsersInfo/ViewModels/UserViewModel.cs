using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UsersInfo.Core;
using UsersInfo.Data;
using UsersInfo.Data.SqlOperations;


namespace UsersInfo.ViewModels
{
   public class UserViewModel : INotifyPropertyChanged
    {
        #region Fields
        private readonly IDataHelper<User> _dataHelper;
        private User user;
        private User selectedUser;
        private ObservableCollection<User> users;
        #endregion


        #region Properties
        public string Name 
        {
            get 
            {
                return user.Name;  
            }
            set 
            {
                if (user.Name != value)
                { 
                    user.Name = value;
                    OnPropertyChanged(nameof(user.Name));
                }
            } 
        }
        public string Email
        {
            get
            {
                return user.Email;
            }
            set
            {
                if (user.Email!= value)
                {
                    user.Email = value;
                    OnPropertyChanged(nameof(user.Email));
                }
            }
        }
        public string PhoneNumber
        {
            get
            {
                return user.PhoneNumber;
            }
            set
            {
                if (user.PhoneNumber != value)
                {
                    user.PhoneNumber = value;
                    OnPropertyChanged(nameof(user.PhoneNumber));
                }
            }
        }
        public User SelectedUser
        {
            get
            {
                return selectedUser;
            }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }
        public ObservableCollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }
        #endregion
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }

        public UserViewModel()
        {
            user = new User();
            users = new ObservableCollection<User>();
            _dataHelper = new UserEntity();

            LoadData();
            AddCommand = new RelayCommand(AddData);
            EditCommand = new RelayCommand(EditData);
            SaveCommand = new RelayCommand(SaveData);
            DeleteCommand = new RelayCommand(DeleteData);
        }

        private async void DeleteData()
        {
            if (SelectedUser != null)
            { 
                await _dataHelper.RemoveAsync(SelectedUser.Id);
                LoadData();      
            }
            else
            {
                MessageBox.Show("Choose user to Delete");
            }
        }

        private async void SaveData()
        {
            if (SelectedUser != null)
            {
                user = new User()
                {
                    Name = Name,
                    Email = Email,
                    PhoneNumber = PhoneNumber,
                    Id= SelectedUser.Id
                    
                };
               await _dataHelper.EditAsync(user);
            }
            else
            {
                user = new User()
                {
                    Name = Name,
                    Email = Email,
                    PhoneNumber = PhoneNumber
                };
           await _dataHelper.AddAsync(user); 
                
            }
            LoadData();
        }

        private void EditData()
        { if (SelectedUser != null)
            {
                SetData();

                Views.AddUser addUser = new Views.AddUser(this);
                addUser.Show();
            }
            else
            {
                MessageBox.Show("Choose user to Edit");
            }

        }

        private void SetData()
        {
            Name = selectedUser.Name;
            Email = selectedUser.Email;
            PhoneNumber = selectedUser.PhoneNumber; 
        }

        private void AddData()
        {
            ClearData();

            Views.AddUser addUser = new Views.AddUser(this);
            addUser.Show();
            
        }

        private void ClearData()
        {
           Name = string.Empty; 
           Email = string.Empty;
           PhoneNumber = string.Empty;  
        }

        private async void LoadData()
        {
            users.Clear();
            foreach (User user in await _dataHelper.GetAllAsync())
            {
                users.Add(user);
            }

        }

        //PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
