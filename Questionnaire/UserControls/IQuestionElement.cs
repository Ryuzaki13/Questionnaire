using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.UserControls {
	public interface IQuestionElement {
		int ID { get; set; }

		string GetTypeName();
		int GetID();
		string[] GetAnswers();
	}
}
