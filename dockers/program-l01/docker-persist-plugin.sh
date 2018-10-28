docker run -d \
    -v /run/docker/plugins/:/run/docker/plugins/ \
    -v /path/to/store/json/for/restart/:/var/lib/docker/plugin-data/ \
    -v /path/to/where/you/want/data/volume/:/path/to/where/you/want/data/volume/ \
        cwspear/docker-local-persist-volume-plugin