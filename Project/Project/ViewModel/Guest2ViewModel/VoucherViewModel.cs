using Project.Model;
using Project.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel.Guest2ViewModel
{
    public class VoucherViewModel : CloseableViewModel, IObserver
    {
        public ObservableCollection<Voucher> _voucher;
        public ObservableCollection<Voucher> Voucher
        {
            get
            {
                return _voucher;
            }

            set
            {
                _voucher = value;
                OnPropertyChanged(nameof(Voucher));
            }
        }

        public VoucherViewModel()
        {
            Voucher = new ObservableCollection<Voucher>();
            PopulateDataGrid();
        }

        public void PopulateDataGrid()
        {
            Voucher.Add(new Voucher { Name = "Voucher 1", ExpirationDate = new DateTime(), Description = "Voucher 1 dobijen 200202020" });
            Voucher.Add(new Voucher { Name = "Voucher 2", ExpirationDate = new DateTime(), Description = "Voucher 2 dobijen 200202020" });
            Voucher.Add(new Voucher { Name = "Voucher 3", ExpirationDate = new DateTime(), Description = "Voucher 3 dobijen 200202020" });
            Voucher.Add(new Voucher { Name = "Voucher 4", ExpirationDate = new DateTime(), Description = "Voucher 4 dobijen 200202020" });
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
