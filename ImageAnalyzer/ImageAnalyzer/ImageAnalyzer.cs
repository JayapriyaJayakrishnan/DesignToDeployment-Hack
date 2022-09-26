using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

using Microsoft.Azure.Functions.Worker;

namespace ImageAnalyzer
{
	public class ImageAnalyzer
    {
        [Function("ImageAnalyzer")]
        public static List<string> Run([BlobTrigger("designconatiner/testdesignblob", Connection = "{BlobConnectionString}")] Stream myBlob)
        {
			string predictionEndpoint = "Enter prediction API endpoint";
			string predictionKey = "Enter prediction key";
			Guid projectId = new Guid("c3712047-9458-44d9-8bfe-c261e63df829#");
			CustomVisionPredictionClient predictionApi = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(predictionKey)) {
				Endpoint = predictionEndpoint
			};
			ImagePrediction result = predictionApi.ClassifyImage(projectId, "Iteration3", myBlob);
			List<string> resourceList = new List<string>();
			foreach (PredictionModel? c in result.Predictions)
			{
				resourceList.Add(c.TagName);
				//Console.WriteLine($"\t{c.TagName}: {c.Probability:P1}");
			}
			return resourceList;
		}
    }
}