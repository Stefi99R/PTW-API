namespace PTW_API.Services
{
    using System;
 	using System.IO;
 
 	using Amazon;
 	using Amazon.SecretsManager;
	using Amazon.SecretsManager.Model;

    public static class RegisterSecretsManager
    {
        public static string GetSecret()
        {
            string secretName = "PTWApiKey";
            string region = "eu-central-1";

            MemoryStream memoryStream = new MemoryStream();

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretName;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

            GetSecretValueResponse? response = null;

            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                throw;
            }
            catch (System.AggregateException ae)
            {
                // More than one of the above exceptions were triggered.
                throw;
            }

            // Decrypts secret using the associated KMS key.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                return response.SecretString;
            }
            else
            {
                memoryStream = response.SecretBinary;

                StreamReader reader = new StreamReader(memoryStream);

                string decodedBinarySecret = System.Text.Encoding.UTF8
                                                   .GetString(Convert.FromBase64String(reader.ReadToEnd()));
                
                return decodedBinarySecret;
            }
        }
    }
}
