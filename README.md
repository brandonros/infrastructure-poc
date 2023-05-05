# infrastructure-poc
Kubernetes YAML deploying ArgoCD applications via Helm charts

## How to deploy

```shell
git clone git@github.com:brandonros/infrastructure-poc.git
cd infrastructure-poc
git checkout vitalii
kubectl apply -f apps/
```

## How to tunnel (ArgoCD)

```shell
# username is admin
# get password
kubectl -n argocd get secret argocd-initial-admin-secret -o json | jq -r '.data.password' | base64 --decode --ignore-garbage
kubectl port-forward svc/argocd-server -n argocd 8080:443
# open browser to https://localhost:8080
```

## How to tunnel (Kubernetes Dashboard)

```shell
# get token
kubectl -n kubernetes-dashboard create token admin-user
kubectl port-forward svc/kubernetes-dashboard -n kubernetes-dashboard 8443:443
# open browser to https://localhost:8443
```

## How to tunnel (Jaeger)

```shell
kubectl port-forward svc/jaeger-jaeger-all-in-one -n jaeger 16686:16686
# open browser to http://localhost:16686
```

## How to tunnel (Grafana)

```shell
# username is admin
# get password
kubectl -n monitoring get secret kube-prometheus-stack-grafana -o json | jq -r '.data["admin-password"]' | base64 --decode --ignore-garbage
kubectl port-forward svc/kube-prometheus-stack-grafana -n monitoring 3000:80
# open browser to http://localhost:3000
```

## How to tunnel (Prometheus)

```shell
kubectl port-forward svc/kube-prometheus-stack-prometheus -n monitoring 9090:9090
# open browser to http://localhost:9090
```

## How to tunnel (Loki)

```shell
kubectl port-forward svc/loki-grafana-loki-gateway -n loki 80:9090
```

## How to tunnel (Redis)

```shell
kubectl -n redis get secret redis -o json | jq -r '.data["redis-password"]' | base64 --decode --ignore-garbage
kubectl port-forward svc/redis-master -n redis 6379:6379
```

## How to build (ConsolePOC)

```shell
cd ConsolePOC
docker build --build-arg ARTIFACTORY_USERNAME --build-arg ARTIFACTORY_PASSWORD -t azurepocsandboxaksacr.azurecr.io/console-poc:0.0.1 .
docker push azurepocsandboxaksacr.azurecr.io/console-poc:0.0.1
 ```

## How to build (MicroservicePOC)

```shell
cd MicroservicePOC
docker build --build-arg ARTIFACTORY_USERNAME --build-arg ARTIFACTORY_PASSWORD -t azurepocsandboxaksacr.azurecr.io/microservice-poc:latest .
docker push azurepocsandboxaksacr.azurecr.io/microservice-poc:latest
```

## How to tunnel (MicroservicePOC1)

```shell
kubectl port-forward svc/microservice-poc1-app -n microservice-poc1 3000:3000
# open browser to http://localhost:3000/swagger/
```

## How to tunnel (MicroservicePOC2)

```shell
kubectl port-forward svc/microservice-poc2-app -n microservice-poc2 3000:3000
# open browser to http://localhost:3000/swagger/
```
