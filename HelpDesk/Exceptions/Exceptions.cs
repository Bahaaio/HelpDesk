using System.Net;

namespace HelpDesk.Exceptions;

public class NotFoundException(string message)
    : BaseException(message, HttpStatusCode.NotFound, "Not Found");

public class UnauthorizedException(string message)
    : BaseException(message, HttpStatusCode.Unauthorized, "Unauthorized");

public class ForbiddenException(string message)
    : BaseException(message, HttpStatusCode.Forbidden, "Forbidden");

public class BadRequestException(string message)
    : BaseException(message, HttpStatusCode.BadRequest, "Bad Request");

public class ConflictException(string message) :
    BaseException(message, HttpStatusCode.Conflict, "Conflict");

public class InternalServerErrorException(string message) :
    BaseException(message, HttpStatusCode.InternalServerError, "Internal Server Error");

public class BaseException(string message, HttpStatusCode statusCode, string title)
    : Exception(message)
{
    public int StatusCode => (int)statusCode;
    public string Title => title;
}
