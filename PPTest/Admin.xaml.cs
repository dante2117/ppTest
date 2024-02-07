using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

namespace PPTest
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        //Вывод тестов
        private void surveyView()
        {
            if (Survey != null) 
            {
                Action action = () =>
                {
                    DataBaseClass dataBaseClass = new DataBaseClass();
                    dataBaseClass.sqlExecute(string.Format("select * from [Survey]"), DataBaseClass.act.select);
                    dataBaseClass.dependency.OnChange += dependencyOnChangeSurveyView;
                    Survey.ItemsSource = dataBaseClass.resultTable.DefaultView;
                    Survey.Columns[0].Visibility = Visibility.Hidden;
                    Survey.Columns[1].Header = "Название";
                };
                Dispatcher.Invoke(action);
            }
        }

        private void dependencyOnChangeSurveyView(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                surveyView();
        }

      
        private void dgSurveyLoaded(object sender, RoutedEventArgs e)
        {
            surveyView();
        }
        private void surveyTabItemLoaded(object sender, RoutedEventArgs e)
        {
            surveyView();
        }
        //Выбор тестов
        private void surveySelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Survey.Items.Count != 0 & Survey.SelectedItems.Count != 0)
            {
                try
                { 
                    DataRowView dataRowView = (DataRowView)Survey.SelectedItems[0];
                    nameSurvey.Text = dataRowView[1].ToString();

                }
                catch
                {

                    MessageBox.Show("Выбрано пустое поле!");
                            
                }
              
            }
        }

        //Добавление теста
        private void addClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute(string.Format("insert into [Survey] ([Name_Survey]) values ('{0}')", nameSurvey.Text), DataBaseClass.act.manipulation);
                nameSurvey.Clear();
                surveyView();
            }

            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }
        //Изменение теста
        private void updateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Survey.Items.Count != 0 & Survey.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)Survey.SelectedItems[0];
                    DataBaseClass dataBaseClass = new DataBaseClass();
                    dataBaseClass.sqlExecute(string.Format("update [Survey] set [Name_Survey] = '{0}' where [ID_Survey] = '{1}'", nameSurvey.Text, dataRowView[0]), DataBaseClass.act.manipulation);
                }
                nameSurvey.Clear();
                surveyView();
            }

            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }
        //Удаление теста
        private void deleteClick(object sender, RoutedEventArgs e)
        {

            try
            {
               switch(MessageBox.Show("Удалить выбранную запись?", DataBaseClass.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if(Survey.Items.Count != 0 & Survey.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)Survey.SelectedItems[0];
                            DataBaseClass dataBaseClass = new DataBaseClass();
                            dataBaseClass.sqlExecute(string.Format("delete from [Survey] where [ID_Survey] = '{0}'", dataRowView[0]), DataBaseClass.act.manipulation);
                        }
                        break;
                }
                nameSurvey.Clear();
                  surveyView();
            }

            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }

        //Вывод модуля
        private void moduleView()
        {
            if (Module != null)
            {
                Action action = () =>
                {
                    DataBaseClass dataBaseClass = new DataBaseClass();
                    dataBaseClass.sqlExecute(string.Format("select * from [Module]"), DataBaseClass.act.select);
                    dataBaseClass.dependency.OnChange += dependencyOnChangeModuleView;
                    Survey.ItemsSource = dataBaseClass.resultTable.DefaultView;
                    Survey.Columns[0].Visibility = Visibility.Hidden;
                    Survey.Columns[1].Header = "Название";
                    Survey.Columns[2].Header = "Тестирование";
                };
                Dispatcher.Invoke(action);
            }
        }

        private void dependencyOnChangeModuleView(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                moduleView();
        }


        private void dgModuleLoaded(object sender, RoutedEventArgs e)
        {
            moduleView();
        }
        private void moduleTabItemLoaded(object sender, RoutedEventArgs e)
        {
            moduleView();
        }
        //Выпадающий список тестов
        private void cbSurveyIDView()
        {
            DataBaseClass dataBaseClass = new DataBaseClass();
            dataBaseClass.sqlExecute(string.Format("select [ID_Survey], [Name_Survey] from [dbo].[Survey] "), DataBaseClass.act.select);
            dataBaseClass.dependency.OnChange += dependencyOnChangeCbSurveyID;
            Action action = () =>
            {
                cbSurveyID.ItemsSource = dataBaseClass.resultTable.DefaultView;
                cbSurveyID.SelectedValuePath = dataBaseClass.resultTable.Columns[0].ColumnName;
                cbSurveyID.DisplayMemberPath = dataBaseClass.resultTable.Columns[1].ColumnName;
            };
            Dispatcher.Invoke(action);
        }
        private void dependencyOnChangeCbSurveyID(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info != SqlNotificationInfo.Invalid)
                cbSurveyIDView();
        }


        //Выбор модуля
        private void moduleSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Module.Items.Count != 0 & Module.SelectedItems.Count != 0)
            {
                try
                {
                    DataRowView dataRowView = (DataRowView)Module.SelectedItems[0];
                    nameModule.Text = dataRowView[1].ToString();
                    cbSurveyID.Text = dataRowView[2].ToString();

                }
                catch
                {

                    MessageBox.Show("Выбрано пустое поле!");

                }

            }
        }

        //Добавление модуля
        private void addModuleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBaseClass dataBaseClass = new DataBaseClass();
                dataBaseClass.sqlExecute(string.Format("insert into [Module] ([Name_Module],[Survey_ID]) values ('{0}', '{1}')", nameSurvey.Text, cbSurveyID), DataBaseClass.act.manipulation);
                nameModule.Clear();
                moduleView();
                //cbSurveyIDView();
            }

            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }
        //Изменение модуля
        private void updateModuleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Module.Items.Count != 0 & Module.SelectedItems.Count != 0)
                {
                    DataRowView dataRowView = (DataRowView)Module.SelectedItems[0];
                    DataBaseClass dataBaseClass = new DataBaseClass();
                    dataBaseClass.sqlExecute(string.Format("update [Module] set [Name_Module] = '{0}', [Survey_ID]  = '{1}' where [ID_Module] = '{2}'", 
                        nameSurvey.Text, cbSurveyID.Text, dataRowView[0]), DataBaseClass.act.manipulation);
                }
                nameModule.Clear();
                moduleView();
            }

            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }
        //Удаление модуля
        private void deleteModuleClick(object sender, RoutedEventArgs e)
        {

            try
            {
                switch (MessageBox.Show("Удалить выбранную запись?", DataBaseClass.App_Name, MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    case MessageBoxResult.Yes:
                        if (Module.Items.Count != 0 & Module.SelectedItems.Count != 0)
                        {
                            DataRowView dataRowView = (DataRowView)Module.SelectedItems[0];
                            DataBaseClass dataBaseClass = new DataBaseClass();
                            dataBaseClass.sqlExecute(string.Format("delete from [Module] where [ID_Module] = '{0}'", dataRowView[0]), DataBaseClass.act.manipulation);
                        }
                        break;
                }
                nameModule.Clear();
                moduleView();
            }

            catch
            {
                MessageBox.Show("Ошибка данных!");
            }
        }
    }
}
