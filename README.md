# infrastructure-poc
Kubernetes YAML deploying ArgoCD applications via Helm charts

## How to deploy

```shell
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
kubectl port-forward svc/jaeger-query -n jaeger 16686:16686
# open browser to https://localhost:16686
```

## How to tunnel (Prometheus)

```shell
kubectl port-forward svc/prometheus-kube-prometheus-prometheus -n monitoring 9090:9090
# open browser to http://localhost:9090
```

## How to tunnel (Grafana)

```shell
# username is admin
# get password
kubectl -n monitoring get secret grafana-admin -o json | jq -r '.data.GF_SECURITY_ADMIN_PASSWORD' | base64 --decode --ignore-garbage
kubectl port-forward svc/grafana -n monitoring 3000:3000
# open browser to http://localhost:3000
```

## How to tunnel (Loki)

```shell
TODO
```

## How to tunnel (Redis)

```shell
TODO
```
