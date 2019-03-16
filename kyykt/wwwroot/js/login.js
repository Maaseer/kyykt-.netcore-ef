const uri = "http://localhost:5000/api/Teacher";
	function login() {
		$.ajax({
			type: "GET",
			url: uri+"?TeacherId=" + $("#user").val() + "&TeacherPasswd=" + $("#pwd").val(),
			cache: false,
			success: function (data) {
				window.sessionStorage["teacherInfo"] = data;
				console.log(data);
				window.location.href = "index.html";
			}
		});
	}
 