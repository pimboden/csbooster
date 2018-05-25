// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using _4screen.CSB.Common;
using _4screen.CSB.Common.Widget;

namespace _4screen.CSB.Widget
{
    public interface IForm
    {
        Guid CreatorUserId { get; set; }
        string FormText { get; set; }
        bool TextBoxShow { get; set; }
        bool TextBoxMust { get; set; }
        bool OptionsMulti { get; set; }
        bool OptionMust { get; set; }
        List<string> OptionTexts { get; set; }
        List<string> FormFieldTexts { get; set; }
        List<bool> FormFieldMusts { get; set; }
        bool AdressShow { get; set; }
        bool AdressCommentShow { get; set; }
        FormAdressSave AdressSave { get; set; }
        bool MustAuthenticated { get; set; }
        string SubjectText { get; set; }
        string PreText { get; set; }
        string PostText { get; set; }
        CarrierType SendCopyToUserAs { get; set; }
        CarrierType SendFormAs { get; set; }
        Guid? ReceptorUserId { get; set; }
        string ReceptorEmail { get; set; }
        bool HasContent { get; set; }
        void FillAdressData();
    }
}