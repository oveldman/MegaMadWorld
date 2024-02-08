#!/bin/bash
sudo docker image prune -f

minikube kubectl -- apply -f Deployments/Kubernetes/Environment/Config.yaml
minikube kubectl -- apply -f Deployments/Kubernetes/Environment/Secrets/Secrets-Local.yaml
minikube kubectl -- apply -f Deployments/Kubernetes/Internal
minikube kubectl -- apply -f Deployments/Kubernetes/Environment/Ingress-Deployment-Local.yaml
minikube kubectl -- apply -f Deployments/Kubernetes/External