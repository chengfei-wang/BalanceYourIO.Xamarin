﻿using System;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace BalanceYourIO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BillRecordDetailPopup : PopupPage
    {
        private BillRecordDetail Detail { get; set; }

        private readonly OnRecordDeleted _listener;

        public BillRecordDetailPopup(BillRecordDetail detail, OnRecordDeleted listener)
        {
            InitializeComponent();

            Detail = detail;

            _listener = listener;

            TimeLabel.Text = DateConverter.ToFriendDateTimeString(Detail.Time);

            TypeIcon.Source = Detail.Type.Icon;

            TypeNameLabel.Text = Detail.Type.Name;

            AmountLabel.Text = $"￥{Detail.Amount:F2}";
            AmountLabel.TextColor = Detail.Color;

            if (Detail.RemarkVisible)
            {
                RemarkLabel.Text = $"备注：{Detail.Remark}";
            }
        }

        private async void DeleteRecord_OnClicked(object sender, EventArgs e)
        {
            BillRecord record = new BillRecord {Id = Detail.Id};
            await App.Database.DeleteBillRecordAsync(record);
            _listener.Invoke();
            await Navigation.PopPopupAsync();
        }

        private async void EditRecord_OnClicked(object sender, EventArgs e)
        {
            //通过BillRecordDetail构造BillRecord
            BillRecord record = new BillRecord
            {
                Id = Detail.Id,
                BillType = Detail.Type.Id,
                Amount = Detail.Amount,
                Remark = Detail.Remark,
                Time = Detail.Time
            };

            await Navigation.PushModalAsync(new AddBillPage(record));
            _listener.Invoke();
            if (PopupNavigation.Instance.PopupStack.Count > 0)
            {
                await Navigation.PopPopupAsync();
            }
        }
    }

    public delegate void OnRecordDeleted();
}