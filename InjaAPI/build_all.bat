@echo "Building Docker Image"
@echo "-------------------------------------------------------------------------------------------"
@echo "-------------------------------------------------------------------------------------------"
docker build -f "C:\Users\guadc\source\repos\DF\mixhub\MixHub\MixHubApp\Dockerfile" --force-rm -t mixhubapp  "C:\Users\guadc\source\repos\DF\mixhub\MixHub"

@echo ""
@echo "Docker Tag to upload to dockerhub"
@echo "-------------------------------------------------------------------------------------------"
docker tag mixhubapp gcdfar/mixhubapp

@echo ""
@echo "Uploading Docker to DockerHub"
@echo "-------------------------------------------------------------------------------------------"
docker push gcdfar/mixhubapp
