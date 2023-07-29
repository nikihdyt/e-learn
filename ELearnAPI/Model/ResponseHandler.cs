using System.Net.NetworkInformation;

namespace ELearnAPI.Model
{
    public class ResponseHandler
    {
        public static Response GetExceptionResponse(Exception e)
        {
            Response response = new Response();
            response.Code = "500";
            response.Data = e.Message;
            return response;
        }
        public static Response GetResponseOk(ResponseType type, object? contract) 
        {
            Response response;

            response = new Response();
            switch (type)
            {
                case ResponseType.Success:
                    response.Code = "200";
                    response.Message = "Success";
                    response.Data = contract;
                    break;
                case ResponseType.NotFound:
                    response.Code = "404";
                    response.Message = "Not Found";
                    response.Data = contract;
                    break;
            }
            return response;
        }
    }
}
