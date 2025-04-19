using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sagdieva_Matie.DB;

namespace Sagdieva_Matie.Windowws
{
    /// <summary>
    /// Логика взаимодействия для AddServiceWindow.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        Service contextService;

        public AddServiceWindow(Service service)
        {
            InitializeComponent();
            var types = DBConnection.matie.TypeService.ToList();
            TypeCB.ItemsSource = types;

            var collections = DBConnection.matie.CollectionService.ToList();
            CollectionCB.ItemsSource = collections;

            DataContext = contextService = service;

        }
        private void AddPhotoBT_Click(object sender, RoutedEventArgs e)
        {
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() == true)
                {
                    string selectedImagePath = $"/pr/{openFileDialog.SafeFileName}";

                    ImageService.Source = new BitmapImage(new Uri(selectedImagePath, UriKind.Relative));
                    contextService.Image = selectedImagePath;
                }
            }
        }


        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            if (contextService.IDProduct == 0)
            {

                DBConnection.matie.Service.Add(contextService);
                DBConnection.matie.SaveChanges();
            }

            var nameText = NameTB.Text.Trim();
            var selectedType = TypeCB.SelectedItem as TypeService;
            var selectedCollection = CollectionCB.SelectedItem as CollectionService;
            var description = DescriptionTB.Text.ToLower();
            var cost = CostTB.Text.ToLower();

            if (string.IsNullOrWhiteSpace(nameText) || selectedCollection == null ||
                string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(cost)
                || selectedType == null)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            try
            {
                contextService.Name = nameText;
                contextService.ID_Type = selectedType.ID;
                contextService.ID_Collection = selectedCollection.ID;
                contextService.Description = description;


                // Заменяем запятую на точку перед парсингом
                string formattedMinCost = cost.Replace(",", ".");

                // Пробуем преобразовать строку в decimal
                if (!decimal.TryParse(formattedMinCost, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedCost))
                {
                    MessageBox.Show($"Ошибка преобразования стоимости: {cost}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверяем, что стоимость не отрицательная
                if (parsedCost < 0)
                {
                    MessageBox.Show("Минимальная стоимость не может быть отрицательной", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Применяем значение в контекст
                contextService.Cost = parsedCost;

                try
                {

                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Ошибка при сохранении в базу данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                DBConnection.matie.SaveChanges();
                DialogResult = true;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void DeleteBT_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBConnection.matie.Service.Remove(contextService);
                DBConnection.matie.SaveChanges();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка удаления!");
                return;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Закрыть окно?",
            "Подтверждение закрытия",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                Window.GetWindow(this)?.Close();
            }
        }
    }
}
