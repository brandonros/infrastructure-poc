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
ARGOCD_PASSWORD=$(kubectl -n argocd get secret argocd-initial-admin-secret -o json | jq -r '.data.password' | base64 --decode --ignore-garbage)
argocd login localhost:8080 --username admin --password "$ARGOCD_PASSWORD" --insecure
argocd app sync microservice-poc1
argocd app sync microservice-poc2
