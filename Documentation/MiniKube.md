### Commands
Start minikube
```bash
minikube start
```
Stop minikube
```bash
minikube stop
```
Load docker image into minikube
```bash
minikube image load madworld/houseapp/weather:lastest
```
Show all images in minikube
```bash
minikube image ls --format table
```
Deploy new configuration in kubernetes
```bash
minikube kubectl -- apply -f Deployement/Kubernetes
```
Show Dashboard
```bash
minikube dashboard
```
Start Tunnel to use the load balancer in the browser
```bash
minikube tunnel
```
To retrieve the external IP and Port
```bash
kubectl get svc
```

## Install
```bash
minikube start
```
```bash
minikube kubectl
```
```bash
minikube addons enable ingress
minikube addons enable metrics-server
```

### References
[Start with MiniKube](https://minikube.sigs.k8s.io/docs/start/)
