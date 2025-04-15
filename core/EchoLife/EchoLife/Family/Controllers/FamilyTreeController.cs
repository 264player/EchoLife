using EchoLife.Common.Exceptions;
using EchoLife.Family.Services;
using Microsoft.AspNetCore.Mvc;

namespace EchoLife.Family.Controllers;

[Route("api")]
[ApiController]
[ExceptionHandling]
public class FamilyTreeController(
    IFamilyTreeService familyTreeService,
    IFamilyMemberService _familyMemberService
) : ControllerBase { }
