#!/bin/bash

set -e

# build
cd MicroservicePOC
docker build --build-arg ARTIFACTORY_USERNAME --build-arg ARTIFACTORY_PASSWORD -t azurepocsandboxaksacr.azurecr.io/microservice-poc:latest .
docker push azurepocsandboxaksacr.azurecr.io/microservice-poc:latest
cd ..
# apply
kubectl apply -f apps/
# TODO: trigger argocd app sync?
