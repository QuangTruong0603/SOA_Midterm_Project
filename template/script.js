function updatePaymentInfo() {
    const studentId = document.getElementById('studentId').value;
    // Giả định lấy thông tin dựa trên studentId, bạn cần thay thế phần này với logic thực tế.
    if (studentId === "123") {
        document.getElementById('studentName').value = localStorage.getItem("username");
        document.getElementById('amount').value = "5000";
    } else {
        document.getElementById('studentName').value = "";
        document.getElementById('amount').value = "";
    }

    checkConditions();
}
function checkConditions() {
    const amount = Number(document.getElementById('amount').value);
    const balance = Number(document.getElementById('balance').value);
    const agree = document.getElementById('agree').checked;

    const isAmountValid = amount > 0 && amount <= balance;
    const canConfirm = agree && isAmountValid;

    document.getElementById('confirmButton').disabled = !canConfirm;
}

document.getElementById('agree').addEventListener('change', checkConditions);