#!/bin/bash
minikube kubectl -n default rollout restart deploy
minikube kubectl -n madworld rollout restart deploy