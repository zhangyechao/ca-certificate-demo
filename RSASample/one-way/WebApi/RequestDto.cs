#nullable disable

namespace WebApi
{
    public class RequestDto
    { 
        /// <summary>
        /// encrypted parameter
        /// </summary>
        public string EP { get; set; }

        /// <summary>
        /// encrypted aes key
        /// </summary>
        public string EAK { get; set; }
    }
}
