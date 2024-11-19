using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5BlazorApp.Models {
    public class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

		public override bool Equals(object obj) {
			if (obj is User otherUser) {
				return this.Id == otherUser.Id;
			}
			return false;
		}

		public override int GetHashCode() {
			return Id.GetHashCode();
		}

	}
}