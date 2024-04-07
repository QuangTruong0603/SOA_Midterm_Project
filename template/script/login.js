document.getElementById("confirmButton").addEventListener("click", function() {
    // Lấy thông tin đăng nhập từ người dùng
    var studentId = document.getElementById("studentId").value;
    var password = document.getElementById("password").value;

    // Tạo một đối tượng chứa thông tin đăng nhập
    var loginInfo = {
        username: studentId,
        password: password
    };

    // Gửi yêu cầu đến API
    fetch('http://localhost:5126/auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginInfo),
        
    })
    .then(response => {
        
        if (!response.ok) {
           
            alert("Đăng nhập không thành công");
        }
        else{
            localStorage.setItem('username', studentId);
            window.location.href = 'account.html';
            return response.json();
        }
        
    })
    // .then(data => {
    //     // Lưu token vào localStorage hoặc sử dụng theo cách khác
    //     var token = data.token;
    //     localStorage.setItem('token', token);
    //     window.location.href = 'index.html';
    // })
   
});
