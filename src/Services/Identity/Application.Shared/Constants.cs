namespace AdventEcho.Identity.Application.Shared;

public static class Constants
{
    public static class Routes
    {
        private const string V1Base = "api/v1";
        
        public static class Account
        {
            private const string Base = $"{V1Base}/account";
            private const string Tag = "Account";
            
            
            /// <summary>
            /// Endpoint: /api/v1/account/confirm-email
            /// </summary>
            public static readonly EndpointDefinition ConfirmEmail = new (
                $"{Base}/confirm-email", 
                "Confirm Email", 
                "Confirm your email",
                Tag
            );
            
            /// <summary>
            /// Endpoint: /api/v1/account/register
            /// </summary>
            public static readonly EndpointDefinition Register = new (
                $"{Base}/register", 
                "Register", 
                "Register a new user account",
                Tag
            );
            
            
            /// <summary>
            /// Endpoint: /api/v1/account/refresh
            /// </summary>
            public static readonly EndpointDefinition Refresh = new (
                $"{Base}/refresh", 
                "Refresh Login", 
                "Refresh login to the system",
                Tag
            );
            
            /// <summary>
            /// Endpoint: /api/v1/account/login
            /// </summary>
            public static readonly EndpointDefinition Login = new (
                $"{Base}/login", 
                "Login", 
                "Login to the system",
                Tag
            );
        }
    }
}