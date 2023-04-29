@echo "Logout and Login to dockerhub

docker logout
docker login -u guadcore -p Esjlf.@.1890

@echo "Building Docker Image"
@echo "-------------------------------------------------------------------------------------------"
@echo "-------------------------------------------------------------------------------------------"
docker build -f "C:\Users\guadcore\source\repos\Personal\Inja\InjaApi\Dockerfile" --force-rm -t inja-api "C:\Users\guadcore\source\repos\Personal\Inja"

@echo ""
@echo "Docker Tag to upload to dockerhub"
@echo "-------------------------------------------------------------------------------------------"
docker tag inja-api guadcore/inja-api:latest

@echo ""
@echo "Uploading Docker to DockerHub"
docker push guadcore/inja-api:latest
@echo "-------------------------------------------------------------------------------------------"