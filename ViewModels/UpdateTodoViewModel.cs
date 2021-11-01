using System.ComponentModel.DataAnnotations;

namespace TodoBasicAPI.ViewModels
{
    public class UpdateTodoViewModel
    {
        [Required(ErrorMessage = "O campo 'Título' é requerido.")]
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}