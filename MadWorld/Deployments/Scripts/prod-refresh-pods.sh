#!/bin/bash
sudo microk8s kubectl -n default rollout restart deploy
sudo microk8s kubectl -n madworld rollout restart deploy

# When new migration is added, then run the following command
sudo microk8s kubectl rollout restart deployment madworld-identity-deployment -n madworld