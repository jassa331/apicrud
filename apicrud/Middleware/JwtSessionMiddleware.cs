//namespace apicrud.Middleware
//{
//    public class JwtSessionMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public JwtSessionMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Session.GetString("JWToken");

//            if (!string.IsNullOrEmpty(token))
//            {
//                context.Request.Headers["Authorization"] = "Bearer " + token;
//            }

//            await _next(context);
//        }
//    }
//}
