using System;

namespace Light.Data.Demo
{
	public class TaskModel
	{
		#region "Data Property"

		private string taskId;

		[DataField ("TaskId")]
		public string TaskId {
			get { 
				return this.taskId; 
			}
			set { 
				this.taskId = value; 
			}
		}

		private short position;

		[DataField ("Position")]
		public short Position {
			get { 
				return this.position; 
			}
			set { 
				this.position = value; 
			}
		}

		private ushort priority;

		[DataField ("Priority")]
		public ushort Priority {
			get { 
				return this.priority; 
			}
			set { 
				this.priority = value; 
			}
		}

		private uint startTime;

		[DataField ("StartTime")]
		public uint StartTime {
			get { 
				return this.startTime; 
			}
			set { 
				this.startTime = value; 
			}
		}

		private uint endTime;

		[DataField ("EndTime")]
		public uint EndTime {
			get { 
				return this.endTime; 
			}
			set { 
				this.endTime = value; 
			}
		}

		private uint contentId;

		[DataField ("ContentId")]
		public uint ContentId {
			get { 
				return this.contentId; 
			}
			set { 
				this.contentId = value; 
			}
		}

		private ushort contentType;

		[DataField ("ContentType")]
		public ushort ContentType {
			get { 
				return this.contentType; 
			}
			set { 
				this.contentType = value; 
			}
		}

		private ushort trans;

		[DataField ("Trans")]
		public ushort Trans {
			get { 
				return this.trans; 
			}
			set { 
				this.trans = value; 
			}
		}

		private string content;

		[DataField ("Content")]
		public string Content {
			get { 
				return this.content; 
			}
			set { 
				this.content = value; 
			}
		}

		#endregion

		public TaskModel ()
		{
		}
	}
}

