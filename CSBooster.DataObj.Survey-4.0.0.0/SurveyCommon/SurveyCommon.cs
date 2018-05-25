// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System.Collections.Generic;

namespace _4screen.CSB.DataObj.SurveyCommon
{
    public enum SurveyAnswersType
    {
        NotSelected = 0,
        MultipleChoiceOnlyOneAnswer = 1,
        MultipleChoiceMultipleAnswers = 2,
        /*
         * NOT IMPLEMENTET YET
          MatrixOfChoicesOnlyOneAnswerPerRow = 3,
                MatrixOfChoicesMultipleAnswersPerRow = 4,
         */
        SingleTextbox = 5,
        Textarea = 6,
        NoAnswers = 7
    }

    public interface ISurveyWizardPage
    {
        Dictionary<string, object> Settings { get; set; }
        Business.DataObjectSurvey survey { get; set; }
        bool SaveSubStep();
        void FillEditForm();
    }

    public enum SurveySemaphore
    {
        Red,
        Yellow,
        Green

    }
}