using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NavDemo.AttachProps
{
    public class RtfText
    {
        public static string GetRichText(DependencyObject obj)
        {
            
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            
            obj.SetValue(RichTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for RichText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(RtfText), new PropertyMetadata(string.Empty, callback));

        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           
            //var reb = (RichEditBox)d;
           // reb.Document.SetText(TextSetOptions.FormatRtf, (string)e.NewValue);
            
             var reb = (RichEditBox)d;
            string str = "";
            reb.Document.GetText(TextGetOptions.FormatRtf, out str);
            if(str != (string)e.NewValue)
                reb.Document.SetText(TextSetOptions.FormatRtf, (string)e.NewValue);
             
             
        }
    }
}
