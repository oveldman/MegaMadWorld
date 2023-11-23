### Commands
Start Dashboard
```bash
sudo microk8s dashboard-proxy --address 0.0.0.0
```
Forward dashboard to port 10443
```bash
microk8s kubectl port-forward -n kube-system service/kubernetes-dashboard 10443:443
```

Enable addons
```bash
sudo microk8s enable registry dashboard ingress metrics-server dns cert-manager
```

Enable cert-manager
```bash
kubectl apply --validate=false -f https://github.com/jetstack/cert-manager/releases/download/v1.11.1/cert-manager.crds.yaml
```

### Edit config
Edit the following path to change the binding path of the pods:\
var/snap/microk8s/current/args/kube-proxy
```bash
--kubeconfig=${SNAP_DATA}/credentials/proxy.config
--cluster-cidr=10.1.0.0/16
--bind-address=0.0.0.0
--healthz-bind-address=127.0.0.1
--profiling=false
```

### Add pods to firewall
You may need to configure your firewall to allow pod-to-pod and pod-to-internet communication:
```bash
sudo ufw allow in on cni0 && sudo ufw allow out on cni0
sudo ufw default allow routed
```

### Open default firewall
```bash
sudo ufw allow 22
sudo ufw allow 80
sudo ufw allow 443
sudo ufw allow 1194
# Open port for kubernetes dashboard
sudo ufw allow in on tun0 to {{InternalIP}} port 10443 proto tcp
sudo ufw enable
sudo ufw reload
```