using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavDemo.Models
{
    public class NavMenuItem : BindableBase<NavMenuItem>
    {
        /// <summary>
        /// FontAwesomeFontFamily
        /// </summary>
        public string Glyph
        {
            get { return _GlyphLocator(this).Value; }
            set { _GlyphLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Glyph Setup        
        protected Property<string> _Glyph = new Property<string> { LocatorFunc = _GlyphLocator };
        static Func<BindableBase, ValueContainer<string>> _GlyphLocator = RegisterContainerLocator<string>("Glyph", model => model.Initialize("Glyph", ref model._Glyph, ref _GlyphLocator, _GlyphDefaultValueFactory));
        static Func<string> _GlyphDefaultValueFactory = () => { return default(string); };
        #endregion

        /// <summary>
        /// 文字
        /// </summary>
        public string Label
        {
            get { return _LabelLocator(this).Value; }
            set { _LabelLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property string Label Setup
        protected Property<string> _Label = new Property<string> { LocatorFunc = _LabelLocator };
        static Func<BindableBase, ValueContainer<string>> _LabelLocator = RegisterContainerLocator<string>("Label", model => model.Initialize("Label", ref model._Label, ref _LabelLocator, _LabelDefaultValueFactory));
        static Func<string> _LabelDefaultValueFactory = () => { return default(string); };
        #endregion
        
        public void Init(string glyph,string label)
        {
            Glyph = glyph;
            Label = label;
        }
    }
}
