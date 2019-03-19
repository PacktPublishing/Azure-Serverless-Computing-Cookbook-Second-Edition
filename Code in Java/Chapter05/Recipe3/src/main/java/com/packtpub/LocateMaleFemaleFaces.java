package com.packtpub;

import com.microsoft.azure.functions.annotation.*;
import com.microsoft.rest.credentials.ServiceClientCredentials;

import okhttp3.*;
import okhttp3.OkHttpClient.Builder;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import com.microsoft.azure.cognitiveservices.vision.computervision.ComputerVisionClient;
import com.microsoft.azure.cognitiveservices.vision.computervision.implementation.ComputerVisionClientImpl;
import com.microsoft.azure.cognitiveservices.vision.computervision.models.*;
import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Azure Blob trigger.
 */
public class LocateMaleFemaleFaces {
    /**
     * This function will be invoked when a new or updated blob is detected at the specified path. The blob contents are provided as input to this function.
     */
    @FunctionName("LocateMaleFemaleFaces")
    @StorageAccount("AzureWebJobsStorage")
    public void run(
        @BlobTrigger(name = "image", path = "images/{name}", dataType = "binary") byte[] image,
        @BindingName("name") String name,
        @TableOutput(name = "maleOputput", tableName = "Male", connection = "AzureWebJobsStorage") OutputBinding<Face> maleOutput,
        @TableOutput(name = "femaleOutput", tableName = "Female", connection = "AzureWebJobsStorage") OutputBinding<Face> femaleOutput,
        final ExecutionContext context
    ) {
        context.getLogger().info("Java Blob trigger function processed a blob. Name: " + name + "\n  Size: " + image.length + " Bytes");
        ComputerVisionClient client = getClient();

        List<VisualFeatureTypes> visualFeatures = new ArrayList<VisualFeatureTypes>();
        visualFeatures.add(VisualFeatureTypes.FACES);
        AnalyzeImageInStreamOptionalParameter optionalParameter = new AnalyzeImageInStreamOptionalParameter();
        optionalParameter.withVisualFeatures(visualFeatures);
        
        client.withEndpoint(System.getenv("CognitiveServicesApiEndpoint"));

        @SuppressWarnings("deprecation")
        ImageAnalysis result = client.computerVision().analyzeImageInStream(image, optionalParameter);
        
        for (FaceDescription faceDescription : result.faces()) {
            FaceRectangle rectangle = faceDescription.faceRectangle();
            Face face = new Face()
                .setTop(rectangle.top())
                .setLeft(rectangle.left())
                .setWidth(rectangle.width())
                .setHeight(rectangle.height())
                .setImage(name);

            if (Gender.MALE.equals(faceDescription.gender())) {
                maleOutput.setValue(face);
            } else {
                femaleOutput.setValue(face);
            }
        }
    }

    private static ComputerVisionClient getClient() {
        return new ComputerVisionClientImpl(
            System.getenv("CognitiveServicesApiEndpoint"),
            new ServiceClientCredentials() {
                @Override
                public void applyCredentialsFilter(Builder builder) {
                    builder.addNetworkInterceptor(
                        new Interceptor() {
                            @Override
                            public Response intercept(Chain chain) throws IOException {
                                Request request = null;
                                Request original = chain.request();
                                Request.Builder requestBuilder = original.newBuilder();
                                requestBuilder.addHeader("Ocp-Apim-Subscription-Key", System.getenv("CognitiveServicesApiKey"));
                                request = requestBuilder.build();
                                return chain.proceed(request);
                            }
                        }
                    );
                }
            }
        );
    }
}
