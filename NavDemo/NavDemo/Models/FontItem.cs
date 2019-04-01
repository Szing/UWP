using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace NavDemo.Models
{
    public class FontItem: BindableBase<FontItem>
    {

        public FontFamily fontFamily { get => _fontFamilyLocator(this).Value; set => _fontFamilyLocator(this).SetValueAndTryNotify(value); }
        #region Property FontFamily fontFamily Setup        
        protected Property<FontFamily> _fontFamily = new Property<FontFamily> { LocatorFunc = _fontFamilyLocator };
        static Func<BindableBase, ValueContainer<FontFamily>> _fontFamilyLocator = RegisterContainerLocator(nameof(fontFamily), m => m.Initialize(nameof(fontFamily), ref m._fontFamily, ref _fontFamilyLocator, () => default(FontFamily)));
        #endregion


        public string fontName { get => _fontNameLocator(this).Value; set => _fontNameLocator(this).SetValueAndTryNotify(value); }
        #region Property string fontName Setup        
        protected Property<string> _fontName = new Property<string> { LocatorFunc = _fontNameLocator};
        static Func<BindableBase, ValueContainer<string>> _fontNameLocator = RegisterContainerLocator(nameof(fontName), m => m.Initialize(nameof(fontName), ref m._fontName, ref _fontNameLocator, () => default(string)));
        #endregion

    }
}
