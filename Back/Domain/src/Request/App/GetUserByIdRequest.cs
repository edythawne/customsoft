using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.Request.App;

public class GetUserByIdRequest {

    [Required] 
    public long Id { set; get; } = 0;

}