# Identityserver

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
A quick tip, the openid scope is always required when using OpenID Connect flows (where you want to receive an identity token)
</p>
<a href="https://www.scottbrady91.com/Identity-Server/Getting-Started-with-IdentityServer-4"> Whole tutorial</a>