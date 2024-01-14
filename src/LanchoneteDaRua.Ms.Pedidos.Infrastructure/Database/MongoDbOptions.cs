using System.ComponentModel.DataAnnotations;

namespace LanchoneteDaRua.Ms.Pedidos.Infrastructure.Database;

public class MongoDbOptions
{
    [Required(AllowEmptyStrings = false)]
    public string DatabaseName { get; set; }
    
    [Required(AllowEmptyStrings = false)]
    public string ConnectionString { get; set; }
}