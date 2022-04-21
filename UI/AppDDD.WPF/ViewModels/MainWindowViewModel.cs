using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AppDDD.Domain.Entities;
using AppDDD.Interfaces;
using AppDDD.WebAPI.Clients.Employees;
using AppDDD.WPF.ViewModels.Base;

namespace AppDDD.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IRepositoryAsync<Employee> _EmployeesRepository;

        #region Title : string - Заголовок

        /// <summary>Заголовок</summary>
        private string _Title = "Главное окно программы";

        /// <summary>Заголовок</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        private IEnumerable<Employee>? _Employees;

        public IEnumerable<Employee>? Employees
        {
            get
            {
                if (_Employees is not null)
                    return _Employees;
                _ = LoadEmployeesAsync();
                return null;
            }
        }

        private async Task LoadEmployeesAsync()
        {
            var employeees = await _EmployeesRepository.GetAllAsync().ConfigureAwait(false);
            Set(ref _Employees, employeees, nameof(Employees));
        }

        public MainWindowViewModel()
        {
            var client = new HttpClient { BaseAddress = new("https://localhost:7290/") };

            var employees = new EmployeesClient(client, null!);
            _EmployeesRepository = employees;
        }
    }
}
