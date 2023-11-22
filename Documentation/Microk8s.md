### Commands
Start Dashboard
```bash
sudo microk8s dashboard-proxy --address 0.0.0.0
```

Enable addons
```bash
sudo microk8s enable registry dashboard ingress metrics-server
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