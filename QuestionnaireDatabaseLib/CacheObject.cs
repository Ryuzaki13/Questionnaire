namespace QuestionnaireDatabaseLib {
	public class CacheObject {
		private bool isModify = false;

		public bool IsModify() {
			return isModify;
		}

		public void Change() {
			isModify = true;
		}

		public void Apply() {
			isModify = false;
		}
	}
}
