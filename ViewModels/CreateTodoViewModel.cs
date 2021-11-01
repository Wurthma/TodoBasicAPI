using System.ComponentModel.DataAnnotations;

namespace TodoBasicAPI.ViewModels
{
    public class CreateTodoViewModel
    {
        [Required(ErrorMessage = "O campo 'Título' é requerido.")]
        public string Title { get; set; }
    }
}