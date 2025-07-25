# Identityserver
### What is JWT (Json Web Token)?
 JWT token is a compact, URL-safe means of representing claims to be transffered between two parties.Encoded with Base64. It's an open standard (RFC 7519) used to securily transmit informationas a Json object. In soimple term a way for a server to identify a user and their permission.

 ### Parts of Jwt Token
 	* Header   (Algorithm and Token type)
  	* Payload  (Data)
   	* Signature (Verification)
    
 ## OAuth Grant Types and Flows:
 - Authorization Code Flow
 - Client Crredential
 - Resource Owner Password
 - Implicit flow

<img width="859" height="641" alt="image" src="https://github.com/user-attachments/assets/8aa2cb7c-9609-4f9f-b666-8e3b2e889e6b" />

## OpenID COnnect:
- Simple Authentication Layer
- Built on Oauth2
- Extension for OAuth Authorization Code flow(grant)
- OpenId Connect End-point
  	* Authorization Endpoint (/authorize)   -> Res[ponsible for Authentication and consent process of enduser
  	* Toekn Endpoint (/token)   -> A critical component used by relaying parties (Clients) to obtain tokens (Access token, ID Token and refresh token. Allows exchange of the client application with Authorization code, ClientId and Client Secret
  	* UserInfo Endpoint (/usernfo)  -> Additional claims requested by the provider. It allows client applications to retrive user profile information after a user has successfullly authenticated with OpenId provider.

## OpenId connect Authentication flow:
	- Authorization Code Flow  -> "Code"
 	- Implicit Flow  -> id_token token
  	- Hybrit Flow -> code, id token or code token or code, id_token


## Identity Server 4 Terminologies:

Identity Server is a .Net based framework for implementing OpenId Connect and OAuth2 in applications
<img width="1043" height="598" alt="image" src="https://github.com/user-attachments/assets/ea6eaa98-1645-4ea5-9bcc-6ee0faa97038" />

<img width="1161" height="680" alt="image" src="https://github.com/user-attachments/assets/c64d4755-38ad-4a1c-b706-04789b30a11e" />

## Identoity Server Flow for Movie WebApi:

<img width="670" height="378" alt="image" src="https://github.com/user-attachments/assets/37c61a7d-f08d-4e2d-ad99-b30c9a90cacf" />


<pre>

	* Install IdentityServer Nuget package into IdentityServer project
	* Install Microsoft.AspNetCore.Authentication.JwtBearer  in Api project
</pre>
<p>
	In IdentityServer Project add a class called Config.cs
	<pre>
		public class Config
		{
		    public static IEnumerable<Client> Clients =>
			new Client[]
			{
			    new Client
			    { 
				ClientId="movieClient",
				AllowedGrantTypes=GrantTypes.ClientCredentials,
				ClientSecrets=
				{ 
				    new Secret("secret".Sha256())
				},
				AllowedScopes={"movieAPI"}
			    }
			};
		
		    public static IEnumerable<ApiResource> ApiResources=>
			new ApiResource[]
			{
			};
		
		    public static IEnumerable<ApiScope> ApiScopes=>
			new ApiScope[]
			{
			    new ApiScope("movieAPI","Movie API")
			};
		
		    public static IEnumerable<IdentityResource> IdentityResources=>
			new IdentityResource[]
			{
			};
		
		    public static List<TestUser> TestUsers=>
			new List<TestUser>
			{
			};
		
		}
	</pre>
 	<p>
  		And in programe.cs add these configuration
    <pre>
    builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    //.AddInMemoryIdentityResources(Config.IdentityResources)
    //.AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    //.AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();
    </pre>
  	</p>
</p>
<b>Basic Configuration for IdentityServer in the startup.cs</b>
<p>
	<pre>
	services.AddIdentityServer()
		.AddInMemoryClients(new List<Client>())
		.AddInMemoryIdentityResources(new List<IdentityResource>())
		.AddInMemoryApiResources(new List<ApiResource>())
		.AddInMemoryApiScopes(new List<ApiScope>())
		.AddTestUsers(new List<TestUser>())
		.AddDeveloperSigningCredential();
		<br>
	public void Configure(IApplicationBuilder app)
	{
		app.UseRouting();
		app.UseIdentityServer();
	}
	</pre>
</p>
<p>
	Identit server is ready in this stage but clients, resources and users are empty. but we can find OpenID Connect discovery document in the end-point /.well-known/openid-configuration
</p>
<p>
	OpenID Connect discover document contains metadata information such as, <br>
	<ul>
		<li>Location of the various end-point (authorization end-point and token end-point)</li>
		<li>location of it's public keys (a JSON Web key set(JWKS))</li>
		<li>Grant types the provide supports</li>
		<li>Scope it can authorize</li>
	</ul>
</p>
<p>
	Your signing credentials are private keys used to sign token. This allows for client applications 
	and protcted resources to verify that the contents of the token have not been altered in transit and that the token was created by identity server.
	<br>
	IdentityServer uses the private key to create signatures, while other applications use the corrresponding public key to verify the signature. This public keys are accessible to the client via the jwks_uri in the OpenID Connect discovery document.
</p>
<p>
	When you go to create and use your own signing credentials, do so using a tool such as OpenSSL or the New-SelfSignedCertificate PowerShell command. You can store the keys in an X.509 certificate, but there shouldn’t be any need to have the certificate issued by a Global CA. IdentityServer is only interested in the private key here and verifiers are typically only concerned with the public key.
</p>
<p>
	First, We need to have a store of client applications that allowed t use IdentityServer, as well as the Protected resources that those clients can use, and the user that can authenticate in our system.
</p>
<br>
<b>Scopes</br><br>
<p>
	Scopes represent what a client application is allows to do.  In IdentityServer, scopes are typically modeled as resources, which come in two flavors.
	<ul>
		<li>
			Identity Resources : By setting the UserClaims property, you are ensuring that these claim types will be added to any access tokens that have this scope (if the user has a value for that type, of course). In this case, you are ensuring that a user’s role claims will be added to any access tokens authorized to use this scope.
				
		</li>
		<li>
			Api Resources : 
				An Api Resorce models a single API that IdentityServer is protecting. In OAuth, this is known as a "protected resource".<br>
				An API scope is an individual authorization level on an API that a client application is allowed to request. For example, an API resource might be adminapi, with the scopes adminapi.read, adminapi.write, and adminapi.createuser. API scopes can be as fine-grained or as generic as you want.
		</li>
	</ul>
</p>
<p>
	An Identity resource allow you to model a scope that will permit a client application to view a subset of a claims about a user. ex. the <b>profie</b> scope enables the app to see the claims about the user such as name and date of birth<br><br>
	An Api resources allows you to model access to an entire protected resource, an Api, with inidivitual permission level that a client application can request access to.
</p>
<p>
	<pre>
	services.AddAuthentication(options =>
    {
        options.DefaultScheme = "cookie";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("cookie")
    .AddOpenIdConnect("oidc", options =>
    {
	    options.Authority = "https://localhost:5000";
        options.ClientId = "oidcClient";
        options.ClientSecret = "SuperSecretPassword";
    
        options.ResponseType = "code";
        options.UsePkce = true;
        options.ResponseMode = "query";
    
        // options.CallbackPath = "/signin-oidc"; // default redirect URI
        
        // options.Scope.Add("oidc"); // default scope
        // options.Scope.Add("profile"); // default scope
        options.Scope.Add("api1.read");
        options.SaveTokens = true;
    });
	</pre>
	here options.SaveTokens = true; makes the identity and access token to be saved and accessible using the code
	<pre>
		HttpConnect.GetTokenAsync("access_token");
	</pre>
</p>
<p>
A quick tip, the openid scope is always required when using OpenID Connect flows (where you want to receive an identity token)
</p>
<a href="https://www.scottbrady91.com/Identity-Server/Getting-Started-with-IdentityServer-4"> Whole tutorial</a>
