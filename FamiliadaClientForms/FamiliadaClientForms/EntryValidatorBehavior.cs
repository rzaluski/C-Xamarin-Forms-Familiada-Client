using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FamiliadaClientForms
{
    public class EntryValidatorBehavior : Behavior<Entry>
    {
        public string Type { get; set; } = null;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += OnEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= OnEntryTextChanged;
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            if (Type == null || entry.Text == null) return;
            string regex = "";
            if(Type == "Digits") regex = @"[^0-9]";
            if (Type == "DigitsAndDot")
            {
                regex = @"[^0-9.,1]";
                entry.Text = Regex.Replace(entry.Text, ",", ".");
            }
            entry.Text = Regex.Replace(entry.Text, regex, string.Empty);
        }
    }
}
