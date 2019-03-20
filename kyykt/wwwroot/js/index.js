function onload(){
    if(window.localStorage.getItem("login")!=1){
        alert('不经过login页面，无法登录index页面');
        window.location.href='login.html'; 
    }
    else
        window.localStorage.setItem("login",'0');
}
