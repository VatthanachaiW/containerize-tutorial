ARG VERSION=7.0-alpine

FROM mcr.microsoft.com/dotnet/sdk:$VERSION as builder
ARG APPNAME=Demo.API

WORKDIR /src
COPY ./$APPNAME ./$APPNAME
RUN dotnet publish "$APPNAME/$APPNAME.csproj" \
	-c Release \
	-o /app/publish \
	-r alpine-x64 \
	--self-contained true \
	/p:PublishReadyToRun=true \
	/p:PublishReadyToRunShowWarnings=true \
	/p:PublishSingleFile=true \
	/p:TargetLatestRuntimePatch=true 
RUN chmod u+x,o+x /app/publish/$APPNAME

FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS final
ARG APPNAME=Demo.API

ENV APP_USER=app \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    DOTNET_RUNNING_IN_CONTAINER=true \
    ASPNETCORE_URLS=http://+:8080;https://+:443 \
    TZ=Asia/Bangkok

RUN apk add libgdiplus --repository https://dl-3.alpinelinux.org/alpine/edge/testing/ && \
    apk add --no-cache icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib tzdata libgdiplus --upgrade bash && \
    addgroup -g 1000 $APP_USER && \
    adduser -u 1000 -G $APP_USER -s /bin/ash -h /home/$APP_USER -D $APP_USER && \
    chown -R $APP_USER:$APP_USER /home/$APP_USER

WORKDIR /home/$APP_USER

COPY ["scripts/hardening/harden.sh", "scripts/hardening/post-install.sh", "./"]

RUN chmod +x harden.sh post-install.sh
CMD	"/harden.sh"
RUN rm harden.sh

COPY --from=builder --chown=$APP_USER:$APP_USER /$APP_USER/publish .
CMD  "/post-install.sh ${APPNAME}"
RUN rm post-install.sh

USER $APP_USER
EXPOSE 8080 443
ENTRYPOINT ["./${APPNAME}"]
