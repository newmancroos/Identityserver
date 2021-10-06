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
