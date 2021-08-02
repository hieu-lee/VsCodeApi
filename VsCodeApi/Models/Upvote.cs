using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VsCodeApi.Models
{
    public class Upvote
    {
        [Key]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ApiId { get; set; }
    }
}
